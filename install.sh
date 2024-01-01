#!/bin/bash

if [[ -f /etc/os-release ]]; then
    source /etc/os-release
    release=$ID
else 
    source /usr/lib/os-release
    release=$ID
fi  
rm -rf 3xui-multi-protocol

if [[ "${release}" == "debian" ]]; then
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
elif [[ "${release}" == "centos" ]]; then 
dnf install wget
dnf install git
git clone https://github.com/M4mmad/3xui-multi-protocol.git
 cd 3xui-multi-protocol/3xui-multi-protocol
  sudo dnf install dotnet-sdk-7.0
  sudo dnf install aspnetcore-runtime-7.0
  sudo dnf install dotnet-runtime-7.0
  dotnet publish -c Release -o /etc/3xui-multi-protocol
elif [[ "${release}" == "fedora" ]]; then
dnf install wget
dnf install git
git clone https://github.com/M4mmad/3xui-multi-protocol.git
 cd 3xui-multi-protocol/3xui-multi-protocol
  sudo dnf install dotnet-sdk-7.0
  sudo dnf install aspnetcore-runtime-7.0
  sudo dnf install dotnet-runtime-7.0
  dotnet publish -c Release -o /etc/3xui-multi-protocol
elif [[ "${release}" == "ubuntu" ]]; then
declare repo_version=$(if command -v lsb_release &> /dev/null; then lsb_release -r -s; else grep -oP '(?<=^VERSION_ID=).+' /etc/os-release | tr -d '"'; fi)
apt install git
git clone https://github.com/M4mmad/3xui-multi-protocol.git
 cd 3xui-multi-protocol/3xui-multi-protocol
  wget https://packages.microsoft.com/config/ubuntu/$repo_version/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
 dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
 apt-get update 
apt-get install -y dotnet-sdk-7.0
apt-get update
apt-get install -y aspnetcore-runtime-7.0
apt-get update
apt-get install -y dotnet-runtime-7.0
apt-get update
  dotnet publish -c Release -o /etc/3xui-multi-protocol
 else
 apt install git
git clone https://github.com/M4mmad/3xui-multi-protocol.git
  wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
  chmod +x ./dotnet-install.sh
  ./dotnet-install.sh --channel 7.0
  apt update
  apt install -y dotnet7
  cd 3xui-multi-protocol/3xui-multi-protocol
  dotnet publish -c Release -o /etc/3xui-multi-protocol
fi

  
 
  cd /etc/systemd/system/
  wget https://raw.githubusercontent.com/M4mmad/3xui-multi-protocol/master/3xui-multi-protocol.service
   cp /etc/x-ui/x-ui.db /etc/x-ui/backup.db
  systemctl daemon-reload
  systemctl start 3xui-multi-protocol
systemctl enable 3xui-multi-protocol
systemctl restart 3xui-multi-protocol
