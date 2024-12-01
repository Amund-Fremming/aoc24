#!bin/bash

n=24

for((i=1; i<n+1; i++)); do
    name="puzzle$i"
    dotnet new console -n "$name"
    cd $name
    dotnet new sln
    dotnet sln add "$name.csproj"
    cd ..
done
