####
This example implements API versioning. Project created in Visual Studio 2022.

####
1.- In Query string.
####
2.- Inside HEADERS key-value pair.
####
3.- Inside URL.

#####
How to test.

#####
1. http://localhost:5051/api/stringlist?api-version=1.0

#####
2. http://localhost:5051/api/stringlist
#####
In HEADERS add
#####
Accept : application/json;ver=2.0

##### 
3.- http://localhost:5051/api/v3/stringlist
