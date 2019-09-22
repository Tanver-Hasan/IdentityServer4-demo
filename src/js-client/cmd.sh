docker build -t identiy-js-client . 
# docker run -it --rm -p 5003:80  identiy-js-client

docker service create --name identiy-js-client -p 5003:80 identiy-js-client