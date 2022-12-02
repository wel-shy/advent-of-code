#! /bin/sh

if [ -z "$SESSION" ]; then
    echo "SESSION environment variable is not set"
    echo "Session cookie can be found in the browser's cookies"
    exit 1
fi

YEAR=$1
DAY=$2
USER=$(git config user.email)

curl --cookie "session=${SESSION}" --header "User-Agent: ${USER}" https://adventofcode.com/$YEAR/day/$DAY/input