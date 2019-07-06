#!/bin/bash

docker build . -t $1
docker tag todo-graph-ql registry.heroku.com/$1/web
heroku container:push web -a $1
heroku container:release web -a $1
