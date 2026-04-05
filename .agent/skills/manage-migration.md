skill:
  name: "manage-migration"
  description: "Adds an EF Core migration and generated script for a specific module context using Makefile."
  inputs:
    - name: module
      description: "The module name (e.g. Products, IAM, Outbox)"
    - name: migration_name
      description: "PascalCase name of migration"

  instructions: |
    1. **Create Migration via Makefile**:
       Run the make command to add the migration for the specific module.
       ```bash
       make ef-add-{{module}} name={{migration_name}}
       ```

    2. **Review**: Check the generated migration files for any unintended or breaking schema changes.

    3. **Generate Idempotent Script**:
       Generate the migration SQL script using the Makefile target.
       ```bash
       make ef-script-{{module}} from={{TheLastMigrationNameBeforeThat}}
       ```

    4. **Add to Virtual Project Folder**:
       Move or copy the newly generated SQL script (output by the make command into `migrations/{{module}}/`) into the virtual project folder for scripts:
       `scripts/{{module}}/` (located at project root). Ensure it is tracked by version control and added to your solution structure if necessary.
