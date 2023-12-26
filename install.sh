#!/bin/bash
apt install git
  git clone https://github.com/M4mmad/3xui-multi-protocol.git
if [[ -f /etc/os-release ]]; then
    source /etc/os-release
    release=$ID
elif [[ -f /usr/lib/os-release ]]; then
    source /usr/lib/os-release
    release=$ID

if [[ "${release}" == "debian" ]]; then
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
  
   fi
    elif [[ "${release}" == "centos" ]]; then 
     cd 3xui-multi-protocol/3xui-multi-protocol
  sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
  sudo yum install dotnet-sdk-7.0
  sudo yum install aspnetcore-runtime-7.0
  sudo yum install dotnet-runtime-7.0
  dotnet publish -c Release -o /etc/3xui-multi-protocol
    fi
    elif [[ "${release}" == "fedora" ]]; then 
    cd 3xui-multi-protocol/3xui-multi-protocol
  sudo dnf install dotnet-sdk-7.0
  sudo dnf install aspnetcore-runtime-7.0
  sudo dnf install dotnet-runtime-7.0
  dotnet publish -c Release -o /etc/3xui-multi-protocol
    fi
else
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
  systemctl daemon-reload
  systemctl start 3xui-multi-protocol
