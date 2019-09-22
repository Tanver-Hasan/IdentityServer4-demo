docker build -t api . 
# docker run -it --rm -p 5003:80  api

docker service create --name api -p 5001:80 api