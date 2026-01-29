# By submitting this assignment, I agree to the following:
# "Aggies do not lie, cheat, or steal, or tolerate those who do."
# "I have not given or received any unauthorized aid on this assignment."
#
# Name: Hayden Zavala
# Section: 535
# Assignment: Lab Topic 7, Team 
# Date: 2nd October 2025
#
#
#

#our lists for the 9 by 9 board
List1 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]
List2 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]
List3 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]
List4 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]
List5 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]
List6 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]
List7 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]
List8 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]
List9 = ["."],["."],["."],["."],["."],["."],["."],["."],["."]


#the numbers listing the columns
print("  1 2 3 4 5 6 7 8 9")

#row counter for printing each seperate row numerically
row_counter = 0
# n is the amount of times we go through each value of each list
n = 1
# j is the list number we are choosing to print 
j = 0

#k is not only how we label each row's value, but also how we loop around for all lists needed
for k in range(1,10):
    row_counter += 1
    print(k, end= " ")
    j = 0
    n = 1
    #there is only 9 lists, so we should never go above that
    while n < 10:
        # having them connect until the 9th list in the list row
        if n < 9:
            #row counter indicating the fact the next list row needs to be printed
            if row_counter == 1:
                print(List1[j][0], end=" ")
                j += 1
                n += 1
                continue
            elif row_counter == 2:
                print(List2[j][0], end=" ")
                j += 1
                n += 1
                continue
            elif row_counter == 3:
                print(List3[j][0], end=" ")
                j += 1
                n += 1
                continue
            elif row_counter == 4:
                print(List4[j][0], end=" ")
                j += 1
                n += 1
                continue
            elif row_counter == 5:
                print(List5[j][0], end=" ")
                j += 1
                n += 1
                continue
            elif row_counter == 6:
                print(List6[j][0], end=" ")
                j += 1
                n += 1
                continue
            elif row_counter == 7:
                print(List7[j][0], end=" ")
                j += 1
                n += 1
                continue
            elif row_counter == 8:
                print(List8[j][0], end=" ")
                j += 1
                n += 1
                continue
            elif row_counter == 9:
                print(List9[j][0], end=" ")
                j += 1
                n += 1
                continue
            #when n finally hits 9, that means we're at the end of the list and don't need to add end=" " anymore so we can have a new line for the next row.
        if n == 9:
            #to end the while loop and start the next k range
            n += 1
            #to make sure we reset the value of j for the next list row 
            if j >= 9:
                j = 0
            elif row_counter == 1:
                print(List1[j][0])
                
            elif row_counter == 2:
                print(List2[j][0])
                
            elif row_counter == 3:
                print(List3[j][0])
                
            elif row_counter == 4:
                print(List4[j][0])
                
            elif row_counter == 5:
                print(List5[j][0])
              
            elif row_counter == 6:
                print(List6[j][0])
                
            elif row_counter == 7:
                print(List7[j][0])
                
            elif row_counter == 8:
                print(List8[j][0])
                
            elif row_counter == 9:
                print(List9[j][0])
                
        


#stop variable will be used to determine whether to stop the cycle or to continue the game
stop = "Howdy"
#turn shows whether the black or white pieces go 
turn = 0
#making sure no craziness happens with turn and only continuing if stop is not true
while turn > -1 and stop != True:
    
    #i is the value we use for printing the whole list at the end
    i = 0
    #j is also that same value we reset to have a fresh start again
    j = 0
    #we add 1 to the turn to switch between odd and even (odd being player 1, even being player 2)
    turn +=1 
    
    #K is the row list value the player wants to put their piece
    K = int(input("Insert a row value from 1-9: "))
    #J is the column list value the player wants to put their piece
    J = int(input("Insert a column value from 1-9: "))
    
    #make sure J is actually in range.
    if J > 0 and J < 10:
        #determine each event for the 9 rows.
        if K == 1:
            #module the turn value to determine if odd or even. Change piece color based on that fact.
            if turn % 2 == 0 and List1[J - 1][0] == ".":
                List1[J - 1][0] = (chr(9679))
            else:
                if List1[J - 1][0] == ".":
                    List1[J - 1][0] = (chr(9675))
                else:
                    #if a stone is already located there
                    print("A stone is already placed there! Try again.")
                    continue
        elif K == 2:
            if turn % 2 == 0 and List2[J - 1][0] == ".":
                List2[J - 1][0] = (chr(9679))
            else:
                if List2[J - 1][0] == ".":
                    List2[J - 1][0] = (chr(9675))
                else:
                    print("A stone is already placed there! Try again.")
                    continue
        elif K == 3:
            if turn % 2 == 0 and List3[J - 1][0] == ".":
                List3[J - 1][0] = (chr(9679))
            else:
                if List3[J - 1][0] == ".":
                    List3[J - 1][0] = (chr(9675))
                else:
                    print("A stone is already placed there! Try again.")
                    continue
        elif K == 4:
            if turn % 2 == 0 and List4[J - 1][0] == ".":
                List4[J - 1][0] = (chr(9679))
            else:
                if List4[J - 1][0] == ".":
                    List4[J - 1][0] = (chr(9675))
                else:
                    print("A stone is already placed there! Try again.")
                    continue
        elif K == 5:
            if turn % 2 == 0 and List5[J - 1][0] == ".":
                List5[J - 1][0] = (chr(9679))
            else:
                if List5[J - 1][0] == ".":
                    List5[J - 1][0] = (chr(9675))
                else:
                    print("A stone is already placed there! Try again.")
                    continue
        elif K == 6:
            if turn % 2 == 0 and List6[J - 1][0] == ".":
                List6[J - 1][0] = (chr(9679))
            else:
                if List6[J - 1][0] == ".":
                    List6[J - 1][0] = (chr(9675))
                else:
                    print("A stone is already placed there! Try again.")
                    continue
        elif K == 7:
            if turn % 2 == 0 and List7[J - 1][0] == ".":
               List7[J - 1][0] = (chr(9679))
            else:
                if List7[J - 1][0] == ".":
                    List7[J - 1][0] = (chr(9675))
                else:
                    print("A stone is already placed there! Try again.")
                    continue
                   
        elif K == 8:
            if turn % 2 == 0 and List8[J - 1][0] == ".":
                List8[J - 1][0] = (chr(9679))
            else:
                if List8[J - 1][0] == ".":
                    List8[J - 1][0] = (chr(9675))
                else:
                    print("A stone is already placed there! Try again.")
                    continue
        elif K == 9:
            if turn % 2 == 0 and List9[J - 1][0] == ".":
                List9[J - 1][0] = (chr(9679))
            else:
                if List9[J - 1][0] == ".":
                    List9[J - 1][0] = (chr(9675))
                else:
                    print("A stone is already placed there! Try again.")
                    continue
        else:
            #for a row input too high or low
            print("Entered an invalid row number, try again.")
        
        #column indicators when printing the whole board again
        print("  1 2 3 4 5 6 7 8 9")
        
        #same as our first printing of the board
        row_counter = 0
        n = 1
        j = 0
        k = 1
        for k in range(1,10):
            row_counter += 1
            print(k, end= " ")
            j = 0
            n = 1
            while n < 10:
                if n < 9:
                    if row_counter == 1:
                        print(List1[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                    elif row_counter == 2:
                        print(List2[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                    elif row_counter == 3:
                        print(List3[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                    elif row_counter == 4:
                        print(List4[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                    elif row_counter == 5:
                        print(List5[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                    elif row_counter == 6:
                        print(List6[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                    elif row_counter == 7:
                        print(List7[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                    elif row_counter == 8:
                        print(List8[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                    elif row_counter == 9:
                        print(List9[j][0], end=" ")
                        j += 1
                        n += 1
                        continue
                if n == 9:
                    n += 1
                    if j >= 9:
                        j = 0
                    elif row_counter == 1:
                        print(List1[j][0])
                    
                    elif row_counter == 2:
                        print(List2[j][0])
                        
                    elif row_counter == 3:
                        print(List3[j][0])
                    
                    elif row_counter == 4:
                        print(List4[j][0])
                    
                    elif row_counter == 5:
                        print(List5[j][0])
                  
                    elif row_counter == 6:
                        print(List6[j][0])
                    
                    elif row_counter == 7:
                        print(List7[j][0])
                    
                    elif row_counter == 8:
                        print(List8[j][0])
                    
                    elif row_counter == 9:
                        print(List9[j][0])
    
    else:
        #what happens when you make a small or massive value for J outside of range
        print("Invalid value for J, try again.")
    
    #looping for the next round unless the player decides to stop entirely
    stop = input("Do you want to continue? Y/N: ")
    if stop == "N" or stop == "No" or stop == "NO" or stop == "no" or stop == "Nope" or stop == "n" or stop == "stop":
        stop = True
        break
    else:
        #for any other input that isn't a variation of No, we loop back again
        continue