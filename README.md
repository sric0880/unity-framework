# unity-framework

# Installation

```sh
mkdir projectName
cd projectName
```

## 1. Setup Gitlab
Click for **[Docker installation docs](https://docs.gitlab.com/omnibus/docker/)**
```sh
# create image
sudo docker pull gitlab/gitlab-ce:latest
# create container, 192.168.1.103 is your localhost ip
sudo docker run -detach \
    --hostname 192.168.1.103 \
    --publish 443:443 --publish 80:80 --publish 22:22 \
    --name gitlab \
    --restart always \
    gitlab/gitlab-ce:latest
# change the config and restart
docker exec -it gitlab vim /etc/gitlab/gitlab.rb
# find external_url. modify to: http://your_hostname/, and then
docker restart gitlab
# if exit non-zero, you may see the logs
docker container logs gitlab
# stop the container
docker container stop gitlab
# start the container
docker container start gitlab
```

## 2. Transfer this github to gitlab

> [How to import projects from github to gitlab](https://docs.gitlab.com/ee/workflow/importing/import_projects_from_github.html)

then `git clone http://192.168.1.103/username/repo_name`

## 3. Setup SVN server
```sh
# create image
docker pull garethflowers/svn-server
# create container
docker run --name svn \
           --detach \
           --volume $Your_Host_Dir:/var/opt/svn \
           --publish 3690:3690 \
           garethflowers/svn-server
```
create repo named resources
```sh
docker exec -it svn svnadmin create resources
```
add user and passwd in file $Your_Host_Dir/resources/conf/passwd  
`username = password`

add at the last line of file $Your_Host_Dir/resources/conf/authz  
`[/]`  
`username = rw`

modify $Your_Host_Dir/resources/conf/svnserve.conf  
`anon-access = none`  
`auth-access = write`  
`password-db = passwd`  
`authz-db = authz`

```sh
# restart svn
docker container restart svn
# add new branch named master
svn mkdir svn://localhost:3690/resources/branches/master -m "add branch master" --parents
# checkout repo to local
svn checkout svn://localhost:3690/resources/branches/master resources
```

## 4. Setup unitybuild tools

> https://github.com/sric0880/unity-buildscripts

And then you can integrate with CI tools like jenkins, buildbot, etc.

Buildbot config file see this: [`master.cfg`](https://gist.github.com/sric0880/230571ef5ecb4883fe08a927adcf20aa)

Then you can build with one command.

# Links
* https://github.com/topameng/tolua
* https://github.com/sric0880/fullserializer
* https://github.com/sric0880/excelconverter
* https://github.com/sric0880/pycsharpmake
* https://github.com/sric0880/tolua
* https://github.com/neuecc/UniRx
