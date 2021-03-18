# JCMFitnessPostgresAPI
This is our new Postgres database API. There are 3 options for Database configuration: local DB using postgres container running on Docker, Azure Database, Heroku Database.
Only thing to change databases is to change the connection string on Startup.cs to different connection string on appSettings.json file. For now I would suggest using Heroku
connection string because Azure DB accepts only certain IP address connections. 

Heroku base address: https://jcmfitnessapi.herokuapp.com
Azure base address: https://jcmfitness1.azurewebsites.net

For now only the relationship between user and workout has been implemented. 

Here are the endpoints for the api:

-----------------------User-------------------
GET     /api/user/getall                                          -get all users
GET     /api/user?userid={id}                                     -get user by id
POST    /api/user                                                 -create new user by posting new user object
PUT     /api/user                                                 -update the user by posting changed user object
DELETE  /api/user?userid={id}                                     -delete user 
GET     /api/user/login?username={username}&password={password}   -verify user login, successful returns user object, if not returns 400 error message
-----------------------Workout-------------
GET     /api/workouts/getall                                      -get all workouts
GET     /api/workouts?workoutid={id}                              -get workout by id
POST    /api/workouts                                             -create new workout by posting new workout object
PUT     /api/workouts                                             -update the workout by posting changed user object
DELETE  /api/workouts?workoutid={id}                              -delete workout 
-----------------------UserWorkout-------------
GET     /api/userworkouts/getall                                  -get all userworkouts
GET     /api/userworkouts?userid={userid}                         -get userworkout by passing user id (returns all the user workouts)
POST    /api/userworkouts?userid={userid}                         -create new userworkout by passing user id and workout object
PUT     /api/userworkouts                                         -update the userworkout passing changed userworkout object
DELETE  /api/userworkouts?userid={userid}&workoutid={workoutid}   -delete one workout associated by one user by passing user and workout id
DELETE  /api/userworkouts/deleteall?userid={userid}               -delete all the workouts associated by the user
