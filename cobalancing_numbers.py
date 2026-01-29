# By submitting this assignment, I agree to the following:
# "Aggies do not lie, cheat, or steal, or tolerate those who do."
# "I have not given or received any unauthorized aid on this assignment."
#
# Name: Hayden Zavala
# Section: 535
# Assignment: Lab Topic 6, 
# Date: 25th September 2025
#
#
#

#variables for the input and loop
n = int(input("Enter a value for n: "))

co_balancing = (n * (n+1))/2 #formula for finding the sums of all numbers to n
r = n + 1
i = 1

#the loop finding co_balancing
while i < 999999 and r != co_balancing:
    r += (n + 1) + i
    i += 1
else:
    if r == co_balancing:
        print(f"{n} is a co-balancing number with r={i}")
    else:
        print(f"{n} is not a co-balancing number")
        