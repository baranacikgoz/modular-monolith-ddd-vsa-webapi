skill:
  name: "add-migration"
  description: "Adds an EF Core migration for a specific module context."
  inputs:
    - name: module
      description: "The module name (e.g. Products)"
    - name: migration_name
      description: "PascalCase name of migration"

  instructions: |
    1. **Execute Command**:
       ```bash
       dotnet ef migrations add {{migration_name}} \
           --project src/Modules/{{module}} \
           --startup-project src/Host \
           --context {{module}}DbContext \
           --output-dir Infrastructure/Persistence/Migrations
       ```

    2. **Review**: Check the generated file for breaking schema changes.
