# By submitting this assignment, I agree to the following:
# "Aggies do not lie, cheat, or steal, or tolerate those who do."
# "I have not given or received any unauthorized aid on this assignment."
#
# Name: Hayden Zavala, 
# Section: 535
# Assignment: Lab Topic 6 Team PLAN, 
# Date: 25th September 2025
#
#
#
import math

#variables
val = float(input("Enter a value for x: "))
while not (0 < val <= 2):
    val = float(input("Out of range! Try again: "))
tol = float(input("Enter the tolerance: "))
ln_x = 0
i = 1


#the real ln value
exact = math.log(val)

#finding approximation of ln_x
while i > -1:
        if abs(((val - 1) ** (i)) / (i)) < tol:
            break
        ln_x += ((val - 1) ** (i)) / (i)
        i += 1
        if abs(((val - 1) ** (i)) / (i)) < tol:
            break
        ln_x -= ((val - 1) ** (i)) / (i)
        #check if smaller than tolerance
        i += 1

#make sure 0 is 0.0
ln_x = float(ln_x)

#absolute value it to make sure the output is positive
difference = abs(ln_x - exact)

#print the results
print(f"ln ({val}) is approximately {ln_x}")
print(f"ln ({val}) is exactly {exact}")
print(f"The difference is {difference}")