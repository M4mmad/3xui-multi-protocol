#!/bin/bash
  apt install git
  git clone https://github.com/M4mmad/3xui-multi-protocol.git
  cd 3xui-multi-protocol/3xui-multi-protocol
  wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb 
  dpkg -i packages-microsoft-prod.deb 
  rm packages-microsoft-prod.deb 
  apt-get update 
  apt-get install -y dotnet-sdk-7.0 
  apt-get install -y aspnetcore-runtime-7.0 
  apt-get install -y dotnet-runtime-7.0 
  apt install libc6 
  dotnet publish -c Release -o /etc/3xui-multi-protocol
  cd /etc/systemd/system/
  wget https://raw.githubusercontent.com/M4mmad/3xui-multi-protocol/master/3xui-multi-protocol.service
  systemctl start 3xui-multi-protocol
  
