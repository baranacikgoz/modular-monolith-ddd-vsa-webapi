# modular-monolith-ddd-vsa-webapi
A .NET 8 Webapi boilerplate with Modular Monolith approach, Domain-Driven Design and Vertical Slices architecture.

## To Run
1. Clone the repo
2. Run `docker compose -f "docker-compose.debug.yml" up -d --build` file (or use vscode docker extension, right click on the file and run)
    1. You may need to create another db called "sonar" for sonarqube (I did it via pgAdmin)
3. Select ``Docker .NET Attach (preview)`` from the debug menu
4. Run the project with cliciking on the green arrow or press ``F5``
5. Open the browser and navigate to ``http://localhost:5000/swagger/index.html``

## How to use Git Hooks to enfoce commit conventions?
