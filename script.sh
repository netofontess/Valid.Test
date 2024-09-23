#!/bin/bash

read -p "Informe o hostname (ex: https://localhost:7076): " hostname

read -p "Informe o caminho do diretório onde as imagens estão armazenadas (sem barra no final): " image_dir

read -p "Digite o token de autenticação: " jwt_token

declare -a curl_requests=(
	'NumeroProtocolo=10001&NumeroVia=1&Cpf=11111111111&Rg=1234567&Nome=João Silva&NomeMae=Maria Silva&NomePai=José Silva&Foto=imagem1.jpg'
	'NumeroProtocolo=10002&NumeroVia=1&Cpf=22222222222&Rg=2345678&Nome=Ana Souza&NomeMae=Clara Souza&NomePai=Marcos Souza&Foto=imagem2.jpg'
	'NumeroProtocolo=10003&NumeroVia=1&Cpf=33333333333&Rg=3456789&Nome=Carlos Pereira&NomeMae=Lucia Pereira&NomePai=Pedro Pereira&Foto=imagem3.jpg'
	'NumeroProtocolo=10004&NumeroVia=1&Cpf=44444444444&Rg=4567890&Nome=Fernanda Lima&NomeMae=Sonia Lima&NomePai=Jorge Lima&Foto=imagem4.jpg'
	'NumeroProtocolo=10005&NumeroVia=1&Cpf=55555555555&Rg=5678901&Nome=Marcos Alves&NomeMae=Eliane Alves&NomePai=Carlos Alves&Foto=imagem5.jpg'
	'NumeroProtocolo=10006&NumeroVia=1&Cpf=66666666666&Rg=6789012&Nome=Patricia Ferreira&NomeMae=Renata Ferreira&NomePai=Claudio Ferreira&Foto=imagem6.jpg'
	'NumeroProtocolo=10007&NumeroVia=1&Cpf=77777777777&Rg=7890123&Nome=Rafael Cardoso&NomeMae=Luciana Cardoso&NomePai=Julio Cardoso&Foto=imagem7.jpg'
	'NumeroProtocolo=10008&NumeroVia=1&Cpf=88888888888&Rg=8901234&Nome=Juliana Ramos&NomeMae=Rosa Ramos&NomePai=Luis Ramos&Foto=imagem8.jpg'
	'NumeroProtocolo=10009&NumeroVia=1&Cpf=99999999999&Rg=9012345&Nome=Thiago Silva&NomeMae=Carla Silva&NomePai=Marcelo Silva&Foto=imagem9.jpg'
	'NumeroProtocolo=10010&NumeroVia=1&Cpf=00000000000&Rg=0123456&Nome=Paulo Oliveira&NomeMae=Isabel Oliveira&NomePai=Rodrigo Oliveira&Foto=imagem10.jpg'
)

for i in "${!curl_requests[@]}"
do
  protocolo=$(echo "${curl_requests[$i]}" | awk -F"&" '{print $1}')
  via=$(echo "${curl_requests[$i]}" | awk -F"&" '{print $2}')
  cpf=$(echo "${curl_requests[$i]}" | awk -F"&" '{print $3}')
  rg=$(echo "${curl_requests[$i]}" | awk -F"&" '{print $4}')
  nome=$(echo "${curl_requests[$i]}" | awk -F"&" '{print $5}')
  nome_mae=$(echo "${curl_requests[$i]}" | awk -F"&" '{print $6}')
  nome_pai=$(echo "${curl_requests[$i]}" | awk -F"&" '{print $7}')

  image_path="$image_dir"

  if [[ -f "$image_path" ]]; then
    echo "Enviando requisição para $hostname/protocolo com a imagem $image_path"
    
    curl -k -X POST "$hostname/protocolo" \
      -H "Authorization: Bearer $jwt_token" \       	  
      -F "NumeroProtocolo=${protocolo#*=}" \
      -F "NumeroVia=${via#*=}" \
      -F "Cpf=${cpf#*=}" \
      -F "Rg=${rg#*=}" \
      -F "Nome=${nome#*=}" \
      -F "NomeMae=${nome_mae#*=}" \
      -F "NomePai=${nome_pai#*=}" \
      -F "Foto=@$image_path"
    
    echo -e "\nRequisição enviada com a imagem $image_path.\n"
  else
    echo "A imagem $image_path não foi encontrada. Pulei esta requisição."
  fi
done

echo "Todas as requisições foram enviadas."

read -p "Pressione ENTER para fechar."
