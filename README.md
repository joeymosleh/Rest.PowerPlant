# Rest.PowerPlant

## build in IIS Express !
1. Clone the Repository.
2. Build and Run the Code (make sure Rest.Powerplant is the Startup Project).
3. Open the browser to http://localhost:8888/swagger to test the API, or you test it through postman.

## Deploy on Docker !
1. Clone the Repository.
2. Make sure Docker Desktop is Installed on the machine.
3. In Rest.Power Project, right click on Dockerfile and Build.
4. When build is done, copy the build name, to use it in the next step.
5. Open terminal in visual studio, and execute the following command and replace the Build Name variable with the real build name: 
```
  docker run -dt -p 8888:80 -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=http://+:80" {{Build Name}}
```
6. Go to Docker dektop, you will find the running container, click on the links or open the browser and navigate to http://localhost:8888/swagger
