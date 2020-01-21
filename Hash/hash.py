# Функции Саши

def hash1 (key):
    key *= key;
    key += 1;
    return key % 1000

def hash2 (key):
    result = 100
    for i in range(50):
        result += key % (i + 1)
    return result % 1000
    
# Функции Илюхи

def hash3 (key):
    # Если у нас есть ограничение на диапазон, то я добавлю остаток от деления
    return ((key << 2) **2) ^ 1537
    
def hash4 (key):
    return ((key + 5)**3) % 1000
    

def find_collision_pairs(n, hash):
    """Ищет коллизии перебором для значений от 0 до n.
    Хэш-функция передаётся как параметр
    """
    l = []
    for i in range(n):
        l.append(hash(i))
        
    for i in range(n - 1):
        for j in range(i, n):
            if (l[i] == l[j]) and (i != j):
                print("Indicies: ", i, ", ", j)
                print("Values: ", l[i], ", ", l[j])
                
def find_collision_for_given(n, hash):
    """Ищет перебором число с таким же хэшем"""
    initial = hash(n)
    print("Initial hash: ", initial)
    i = 0
    
    while True:
        if (hash(i) == initial) and (i != n):
            break
        i += 1
    
    print("Found: ", i)

if __name__ == "__main__":
    find_collision_pairs(1000, hash4)
    # find_collision_for_given(1337, hash2)
                