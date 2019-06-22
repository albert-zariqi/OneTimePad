import random
kerkesa = input("Jepni nje fjali per enkriptim: ")


def convert_decimal_to_bits(stringu):
    lista = list(stringu)
    lista1 =[]
    for karkateri in lista:
        lista1.append(ord(karkateri))
    i = 0
    lista2 = []
    while(i<len(lista1)):
        n = int(lista1[i])
        
        lista3 = []
        
        varguBitave =""
        while (n != 0):
            if(n%2 == 0):
                varguBitave += "0"
            elif(n%2==1):
                varguBitave += "1"
            else:
                continue
            n = int(n/2)
        if(len(varguBitave)<7):
            varguBitave +="0";
        if(len(varguBitave)<8):
            varguBitave += "0"
        lista3 = list(varguBitave)
        k = 0
        j = len(lista3) - 1
        lista4 = []
        
        while(k<len(lista3)):
            lista4.append(lista3[j])
            k +=1
            j -=1
        str1 = ''.join(lista4)
        lista2.append(str1)
        i +=1;

    return lista2

lista5 = convert_decimal_to_bits(kerkesa)
stringu = ''.join(lista5)
lista6 = list(stringu)

def generating_random_key(lista):
    n = len(lista)
    i = 0
    lista1 =[]
    while i<n:
        lista1.append(str(random.randint(0,1)))
        i +=1
    return lista1


lista7 = generating_random_key(lista6)

def encryption(listaPlain, listaKey):
    print(listaPlain)
    print(listaKey)
    lista8 = []
    n = len(listaPlain)
    i =0
    while (i<n):
        p = listaPlain[i]
        q = listaKey[i]
        if(p == 0 and q == 0):
            lista8.append('0')
        elif(p == 1 and q == 1):
            lista8.append('0')
        elif(p == 1 and q == 0):
            lista8.append('1')
        elif(p == 0 and q == 1):
            lista8.append('1')
        else:
            lista8.append("")
        i +=1
    return lista8

#print(lista6)
#print(lista7)
print(encryption(lista6,lista7))

