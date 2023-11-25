import os

def read_file(path):
    # open and read test.txt
    with open(os.path.abspath(path), 'r') as f:
        return f.readlines()