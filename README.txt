# JCMFitnessPostgresAPI
This is our new Postgres database API. There are 3 options for Database configuration: local DB using postgres container running on Docker, Azure Database, Heroku Database.
Only thing to change databases is to change the connection string on Startup.cs to different connection string on appSettings.json file. For now I would suggest using Heroku
connection string because Azure DB accepts only certain IP address connections. 

Heroku base address: https://jcmfitnessapi.herokuapp.com
Azure base address: https://jcmfitness1.azurewebsites.net

For now only the relationship between user and workout has been implemented. 

Here are the endpoints for the api:

-----------------------User-------------------
GET     /api/user              -get all users
GET     /api/user/{id}         -get user by id
POST    /api/user              -create new user by posting new user object
PUT     /api/user              -update the user by id
DELETE  /api/user/{id}         -delete user by id

-----------------------Workout-------------------
GET     /api/workout              -get all workouts
GET     /api/workout/{id}         -get workout by id
POST    /api/workout              -create new workout by posting new workout object
PUT     /api/workout              -update the workout by id
DELETE  /api/workout/{id}         -delete workout by id

-----------------------UserWorkout-------------------
GET     /api/userworkout              -get all userworkouts
GET     /api/userworkout/{id}         -get userworkout by passing user id (returns all the user workouts)
POST    /api/userworkout              -create new userworkout by passing user id and workout id
PUT     /api/userworkout              -update the userworkout by id
DELETE  /api/userworkout/{id}         -delete userworkout by id
