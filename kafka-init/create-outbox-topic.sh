#!/bin/sh

# Configuration
BOOTSTRAP_SERVER="mm.kafka:9092"
MAIN_TOPIC="outbox_topic.Outbox.OutboxMessages"
DLQ_TOPIC="${MAIN_TOPIC}_dlq"
INTEGRATION_MAIN_TOPIC="outbox_topic.Outbox.IntegrationEventOutboxMessages"
INTEGRATION_DLQ_TOPIC="${INTEGRATION_MAIN_TOPIC}_dlq"
# Use multiple partitions to allow parallel consumption for scalability.
# Each partition can be consumed by one consumer instance in a consumer group.
PARTITIONS=3
REPLICATION_FACTOR=1
# Use same retention for DLQ for now, adjust if needed
RETENTION_MS=604800000
MAIN_CLEANUP_POLICY="delete"
DLQ_CLEANUP_POLICY="delete"

# Function to ensure a topic exists
ensure_topic_exists() {
  local topic_name="$1"
  local partitions="$2"
  local replication_factor="$3"
  local cleanup_policy="$4"
  local retention_ms="$5"

  echo "Checking if topic '$topic_name' exists..."
  /opt/bitnami/kafka/bin/kafka-topics.sh --bootstrap-server "$BOOTSTRAP_SERVER" \
    --describe --topic "$topic_name" > /dev/null 2>&1

  if [ $? -eq 0 ]; then
    echo "Topic '$topic_name' already exists."
    return 0
  else
    echo "Topic '$topic_name' not found. Creating..."
    /opt/bitnami/kafka/bin/kafka-topics.sh --bootstrap-server "$BOOTSTRAP_SERVER" \
      --create --topic "$topic_name" \
      --partitions "$partitions" --replication-factor "$replication_factor" \
      --config cleanup.policy="$cleanup_policy" --config retention.ms="$retention_ms"

    if [ $? -eq 0 ]; then
        echo "Topic '$topic_name' created successfully."
        return 0
    else
        echo "ERROR: Failed to create topic '$topic_name'." >&2
        return 1
    fi
  fi
}

# --- Main Script Logic ---
echo "Starting Kafka topic initialization..."

ensure_topic_exists "$MAIN_TOPIC" "$PARTITIONS" "$REPLICATION_FACTOR" "$MAIN_CLEANUP_POLICY" "$RETENTION_MS" || exit 1
ensure_topic_exists "$DLQ_TOPIC" "$PARTITIONS" "$REPLICATION_FACTOR" "$DLQ_CLEANUP_POLICY" "$RETENTION_MS" || exit 1
ensure_topic_exists "$INTEGRATION_MAIN_TOPIC" "$PARTITIONS" "$REPLICATION_FACTOR" "$MAIN_CLEANUP_POLICY" "$RETENTION_MS" || exit 1
ensure_topic_exists "$INTEGRATION_DLQ_TOPIC" "$PARTITIONS" "$REPLICATION_FACTOR" "$DLQ_CLEANUP_POLICY" "$RETENTION_MS" || exit 1

echo "Kafka topic initialization completed successfully."
exit 0
