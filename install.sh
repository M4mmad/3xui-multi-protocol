#!/bin/bash
  apt install git
  git clone https://github.com/M4mmad/3xui-multi-protocol.git
  wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
  chmod +x ./dotnet-install.sh
  ./dotnet-install.sh --channel 7.0
  apt update
  apt install -y dotnet7
  cd 3xui-multi-protocol/3xui-multi-protocol
  dotnet publish -c Release -o /etc/3xui-multi-protocol
  cd /etc/systemd/system/
  wget https://raw.githubusercontent.com/M4mmad/3xui-multi-protocol/master/3xui-multi-protocol.service
  systemctl daemon-reload
  systemctl start 3xui-multi-protocol
  
