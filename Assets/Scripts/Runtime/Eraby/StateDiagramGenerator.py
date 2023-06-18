"""
This module is used to generate a mermaid state machine diagram from a given folder of .cs files.
All .cs files in the folder are parsed and the state machine is generated from the classes found.
A mermaid diagram is then generated and saved to the same folder in .md format.
A class is considered a state if it inherits from 'State' or from another state class.
A transition exists from state A to state B if state A has a call to 'setState<B>()' somewhere in its code or in the code of its parent class.
"""

import os
import re
import sys
import argparse

# Regex to find all classes that inherit from State
stateClassRegex = re.compile(r"class\s+(\w+)\s*:\s*State")
# Regex to find all calls to setState
setStateRegex = re.compile(r"[s|S]etState<(\w+)>\(\)")
# Regex to get the parent class of a class
parentClassRegex = re.compile(r"class\s+(\w+)\s*:\s*(\w+)")



states = set()
transitions = {}
parentClasses = {}
childClasses = {}

def getClassAndParentClass(fileContents):
    pairs = parentClassRegex.findall(fileContents)
    return ({child: parent for (child, parent) in pairs})
        

def getTransitions(fileContents):
    stateName = parentClassRegex.findall(fileContents)[0][0]
    print(stateName)
    _transitions = setStateRegex.findall(fileContents)
    print(stateName, _transitions)
    transitions[stateName] = _transitions

    return transitions

# for each file in the given folder, get all classes and their parent classes
# if a class inherits from State, add it to the list of states
def getStates(folder):
    for filename in os.listdir(folder):
        if filename.endswith(".cs"):
            with open(file=os.path.join(folder, filename), mode="r", encoding="UTF-8") as file:
                fileContents = file.read()
                stateClasses = stateClassRegex.findall(fileContents)
                for stateClass in stateClasses:
                    states.add(stateClass)
                pairs = parentClassRegex.findall(fileContents)
                parentClasses.update(pairs)
                for (child, parent) in pairs:
                    if parent in childClasses:
                        childClasses[parent].append(child)
                    else:
                        childClasses[parent] = [child]
                getTransitions(fileContents)
    return states

# for each state, recursively add all of its child states to the list of states
def addChildStatesRecursive(state):
    if state in childClasses:
        for child in childClasses[state]:
            states.add(child)
            addChildStatesRecursive(child)

def addParentTransitionsRecursive(state):
    if state in parentClasses:
        parent = parentClasses[state]
        if parent != "State":
            addParentTransitionsRecursive(parent)
        if parent in transitions:
            for transition in transitions[parent]:
                transitions[state].append(transition)


def generateMermaidDiagram():
    global transitions
    mermaidDiagram = "stateDiagram-v2\n"
    filterAbstractClasses()
    # filter out any transitions that don't have both states in the set of states
    transitionsCopy = transitions.copy()
    for state in transitionsCopy:
        if state not in states:
            del transitions[state]
        else:
            transitions[state] = [transition for transition in transitions[state] if transition in states]
            # remove duplicate transitions
            transitions[state] = list(set(transitions[state]))
    

    # create an ordered dict from transitions
    sortedtransitions = {state: transitions[state] for state in sorted(transitions)}
    # states with transitions from idle should be moved to the front
    transitions = sortedtransitions
    idleTransitions = transitions["ErabyIdleState"]
    del transitions["ErabyIdleState"]
    transitions = {"ErabyIdleState": idleTransitions, **transitions}



    # remove duplicate transitions
   

    for state in states:
        if state in transitions:
            for transition in transitions[state]:
                mermaidDiagram += f"\t{state} --> {transition}\n"
    
    # remove 'Eraby' and 'State' from the state names in the diagram
    mermaidDiagram = mermaidDiagram.replace("Eraby", "")
    mermaidDiagram = mermaidDiagram.replace("State", "")
    # add 'Idle' as the starting state
    mermaidDiagram = mermaidDiagram.replace("stateDiagram-v2", "stateDiagram-v2\n\t[*] --> Idle\n")
    return mermaidDiagram

def filterAbstractClasses():
    # set states to a new set with all states with "Abstract" in their name removed
    global states
    states = {state for state in states if "Abstract" not in state}


def main():
    parser = argparse.ArgumentParser(description="Generate a mermaid state machine diagram from a given folder of .cs files.")
    parser.add_argument("folder", help="The folder containing the .cs files to parse.")
    args = parser.parse_args()
    folder = args.folder
    if not os.path.isdir(folder):
        print(f"Error: {folder} is not a valid folder.")
        sys.exit(1)
    getStates(folder)
    # copy the set of states so we can iterate over it while adding new states
    statesCopy = states.copy()
    for state in statesCopy:
        addChildStatesRecursive(state)
    statesCopy = states.copy()
    for state in statesCopy:
        addParentTransitionsRecursive(state)
    mermaidDiagram = generateMermaidDiagram()
    with open(os.path.join(folder, "stateMachineDiagram.md"), "w") as file:
        file.write("# State Machine Diagram\n")
        file.write("```mermaid\n")
        file.write(mermaidDiagram)
        file.write("\n```")
    print(f"Generated state machine diagram in {folder}.")
    print(mermaidDiagram)
    print("Done.")
        


if __name__ == "__main__":
    main()


