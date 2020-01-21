# -*- coding: utf-8 -*-
"""
Created on Fri Dec 20 22:58:28 2019

@author: Вера
"""

# SRP-6 Protocol
# N большое безопасное простое число (N = 2q+1, где q-простое число).
# Вся арифметика делается по модулю N.
# g генератор по модулю N
# параметр-множитель k (k = H (N, g) в SRP-6a, k = 3 для унаследованного SRP-6)
# s соль
# I Username - идентификатор пользователя в системе сервера
# P пароль пользователя
# H () хэш-функция
# a, b приватные одноразовые переменные
# А вычисляется на стороне клиент-cервер, B вычисляется на стороне сервер-клиент
# X закрытый ключ (производный от p и s)
# v верификатор паролей на стороне сервера
 
import random
import string

def isPrime(n):
    if n == 2 or n == 3: return True
    if n % 2 == 0 or n < 2: return False
    for i in range(3, int(n ** 0.5) + 1,2):   # вычисление простого
        if n % i == 0:
            return False    
    return True

#def randomString(stringLength):
#   letters = string.ascii_lowercase
#    return ''.join(random.choice(letters) for i in range(stringLength))

primes = [i for i in range(1,100) if isPrime(i)]
q_prime = random.choice(primes) 
n_prime = 2 * q_prime + 1

g_list = [i for i in range(2, n_prime - 2)]
g_modulus = random.choice(g_list)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                


k = 3 # константа для установленного SRP-6

#Клиент
password = random.choice(range(1,10000))
salt = random.choice(range(1,10000))
x = hash(salt + password)
verifier = g_modulus ** x % n_prime
print('Пароль пользователя:', password) 
print('Соль пользователя:', salt)
print('Хэш от соли и пароля:', x)

X = g_modulus ** x % n_prime
if isinstance(X, int):
    valid_X = X

user = 'username'
a = random.choice(range(1,10000))
full_A = g_modulus ** a %n_prime

if full_A != 0:
    print('A != 0. Проверка на сервере прошла успешно.')
    b = random.choice(range(1,100))
    full_B = (k * verifier + g_modulus ** b % n_prime) % n_prime
    server_answer = [salt, full_B]
else:
    print('Проверка на стороне сервера не прошла! А = 0 :(')

if server_answer[1] != 0:
    client_answer = True

server_scrambler = hash(full_A + server_answer[1])
client_scrambler = hash(full_A + server_answer[1])

if server_scrambler != client_scrambler and client_answer:
    print('Соединение с сервером прервано!')
else:
    print('Соединение с сервером прошло успешно!')

new_x = hash(salt + 645)
#new_x = hash(salt + password)


 # Сравнение ключей сессии. Если равны, то проверка пройдена
session_key_client = ((full_B - k * (g_modulus ** new_x % n_prime)) ** (a + client_scrambler * new_x)) % n_prime
session_key_server = ((full_A * (verifier ** server_scrambler % n_prime)) ** b) % n_prime

session_key_hash_client = hash(session_key_client)
session_key_hash_server = hash(session_key_server)

if session_key_hash_client == session_key_hash_server:
    print('Первая проверка прошла успешно!')
else:
    print('Первая проверка провалилась!')

# Клиент генерирует подтвверждение и отправляет его серверу. Сервер у себя генерирует подтверждение
# используя копию общего ключа. Еси вычиления равны, то проверка пройдена
hash_for_m = bool(hash(n_prime)) != (hash(g_modulus))

match_client = hash(hash_for_m + hash(user)+ salt + full_A + full_B + session_key_hash_client)
match_server = hash(hash_for_m + hash(user)+ salt + full_A + full_B + session_key_hash_server)

if match_client == match_server:
    print('Вторая проверка прошла успешно!')
    return_server = hash(full_A + match_server + session_key_hash_server)
    answer_client = hash(full_A + match_client + session_key_hash_client)
    if return_server == answer_client:
        print('Авторизация прошла успешно!')
    else:
        print('Авторизация не удалась!')
else: 
    print('Вторая попытка проверки провалилась!')