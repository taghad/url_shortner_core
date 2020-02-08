# url_shortner_core
Shorten, create and share trusted, powerful links for your business.

## Build

### Migration
```
cd src
dotnet tool install --global dotnet-ef
dotnet ef migrations add UrlShortener.AppDbContext
dotnet ef database update
```
### Run
```
dotnet run
or
dotnet watch run #dotnet watch is a development time tool that runs a dotnet command when source files change.
```

## Running the tests
```
cd test
dotnet test
```

### Coding style I use in this project is:

[alibaba-academy coding conventions](https://github.com/alibaba-academy/coding-conventions)

## Authors

* **MohamadHasan Taghadosi** - *Initial work* - [taghad](https://github.com/taghad)
