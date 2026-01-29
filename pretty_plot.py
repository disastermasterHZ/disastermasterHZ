# By submitting this assignment, I agree to the following:
# "Aggies do not lie, cheat, or steal, or tolerate those who do."
# "I have not given or received any unauthorized aid on this assignment."
#
# Name: Hayden Zavala
# Section: 535
# Assignment: Lab Topic 12, Activity 1
# Date: 7th November 2025
#
#
#

import matplotlib.pyplot as mpl
import numpy as np

mpl.plot([1,2,3,4,5])
m = np.array([(1.02, 0.095), (-0.095, 1.02)])
v = np.array([([0], [1])])
x_cords = []
y_cords = []
print(v[0][0])
print(v[0][1])
x_cords.append(v[0,0])
y_cords.append(v[0,1])
for i in range (250):
    v = m @ v
    x_cords.append(v[0][0])
    y_cords.append(v[0][1])
    
    
    

mpl.plot(x_cords, y_cords, 'o', markersize=2)
mpl.title("Traced spiral")
mpl.xlabel("X-Axis")
mpl.ylabel("Y-Axis")
mpl.grid(True)  
mpl.show()