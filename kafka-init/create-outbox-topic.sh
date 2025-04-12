#!/bin/sh

# Configuration
BOOTSTRAP_SERVER="mm.kafka:9092"
MAIN_TOPIC="outbox_topic.Outbox.OutboxMessages"
DLQ_TOPIC="${MAIN_TOPIC}_dlq" # Convention: Append _dlq
PARTITIONS=1
REPLICATION_FACTOR=1
# Use same retention for DLQ for now, adjust if needed
RETENTION_MS=604800000
MAIN_CLEANUP_POLICY="delete" # Keep as delete unless compaction needed for main topic
DLQ_CLEANUP_POLICY="delete" # Usually delete for DLQ is fine

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
    return 0 # Success (already exists)
  else
    echo "Topic '$topic_name' not found. Creating..."
    /opt/bitnami/kafka/bin/kafka-topics.sh --bootstrap-server "$BOOTSTRAP_SERVER" \
      --create --topic "$topic_name" \
      --partitions "$partitions" --replication-factor "$replication_factor" \
      --config cleanup.policy="$cleanup_policy" --config retention.ms="$retention_ms"

    if [ $? -eq 0 ]; then
        echo "Topic '$topic_name' created successfully."
        return 0 # Success (created)
    else
        echo "ERROR: Failed to create topic '$topic_name'." >&2
        return 1 # Failure
    fi
  fi
}

# --- Main Script Logic ---
echo "Starting Kafka topic initialization..."

# Ensure Main Topic exists
ensure_topic_exists "$MAIN_TOPIC" "$PARTITIONS" "$REPLICATION_FACTOR" "$MAIN_CLEANUP_POLICY" "$RETENTION_MS"
main_topic_result=$?

if [ $main_topic_result -ne 0 ]; then
    echo "Failed to ensure main topic exists. Exiting." >&2
    exit 1
fi

# Ensure DLQ Topic exists
ensure_topic_exists "$DLQ_TOPIC" "$PARTITIONS" "$REPLICATION_FACTOR" "$DLQ_CLEANUP_POLICY" "$RETENTION_MS"
dlq_topic_result=$?

if [ $dlq_topic_result -ne 0 ]; then
    echo "Failed to ensure DLQ topic exists. Exiting." >&2
    exit 1
fi

echo "Kafka topic initialization completed successfully."
exit 0
