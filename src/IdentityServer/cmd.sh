docker build -t identityserver4 .
# docker run -it --rm -p 5000:80  identityserver4

docker service create --name identityserver4  -p 5000:80 identityserver4