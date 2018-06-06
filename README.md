# api seed
A simple but complete dotnet core project seed using a MongoDB database.

![api](https://media.giphy.com/media/OTS4tXJlzyPZK/giphy.gif)

### Requirements 
## Dev system requirements:
You must have the following intalled in your machine:

- [dotnet core](https://www.microsoft.com/net/download/)
- [mongodb](https://www.mongodb.com/download-center?#community)

This project was built using:

| Program       | Version       |
| ------------- |:-------------:|
| dotnet core   | 2.1.4         |
| mongodb       | 3.6.5         |


### Get started

First restore all packages necessary using:
```
dotnet restore
```

To run the development server use:
By deafult it will run on `localhost:5000`
```
dotnet watch run
```

To run the mongodb server run:
By deafult it will run on `localhost:27017`
(make sure `mongod` is on your path after installation)
```
mongod  
```

To install a new package you can use:
```
dotnet add package <package name> --version <package version>
```





