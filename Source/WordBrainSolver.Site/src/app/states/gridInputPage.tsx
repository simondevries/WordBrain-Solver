import * as React from 'react';

interface IGridInputPageProps {
    setShowResultsFunction,
    setIsLoadingFunction,
    setErrorMessageFunction,
    setFoundWordsFunction
};

interface IGridInputPageState {
    gridSize;
    wordLengths;
    result;
    inputErrorMessage;
}

export class GridInputPage extends React.Component<IGridInputPageProps, IGridInputPageState> {
    boardInputGrid = [];

    constructor() {
        super();

        this.initialize();
    }

    initialize() {
        var size = '3';
        this.boardInputGrid = this.makeArray(size, size, '');

        this.state = {
            gridSize: size,
            wordLengths: [''],
            result: this.boardInputGrid,
            inputErrorMessage: ''
        }
        this.forceUpdate();
    }
    //state gridSize
    //wordLength
    //errormessage

    // React components are simple functions that take in props and state, and render HTML
    render() {
        return (
            <div>{/* React components must have a wrapper node/element */}
                <h2>Grid size:</h2>
                <div>
                    <input className='number-input-field' value={this.state.gridSize} onChange={(event) => this.setGridSize(event)} maxLength={1} />
                </div>
                <h2>Word Lengths:</h2>
                <ul>
                    {
                        this.state.wordLengths.map((res, index) => (
                            <input className='number-input-field word-length-input' value={this.state.wordLengths[index]} onChange={(event) => this.setWordLengths(event, index)} maxLength={1} />
                        ))
                    }
                </ul>
                <h2>Grid:</h2>
                <ul className='input-grid'>
                    {
                        this.boardInputGrid.map((res, index) => (
                            <li>
                                {res.map((resTwo, indexTwo) => (
                                    <input id={index + '' + indexTwo} className='grid-input-field number-input-field' onChange={(event) => this.setResult(index, indexTwo, event)} maxLength={1} />
                                ))}
                            </li>
                        ))
                    }
                </ul>
                <div className='bottom-div'>
                    <button className='my-button' onClick={(event) => this.getResults()}>Get results</button>
                </div>
                <h3>{this.state.inputErrorMessage}</h3>
            </div>
        );
    }

    setGridSize(event) {
        if (event.target.value > 5) {
            this.setState({ inputErrorMessage: 'You cannot set a grid size greater than 5' })
            return;
        } else {
            this.setState({ inputErrorMessage: '' })
        }

        this.boardInputGrid = this.makeArray(event.target.value, event.target.value, '');
        this.setState({ gridSize: event.target.value });
    }


    setWordLengths(event, index) {
        var wordLengths = this.state.wordLengths;
        if (index === wordLengths.length - 1) {
            wordLengths.push('');
        }
        wordLengths[index] = event.target.value;
        this.setState({ wordLengths: wordLengths });
    }

    makeArray(w, h, val) {
        var arr = [];
        for (var i = 0; i < h; i++) {
            arr[i] = [];
            for (var j = 0; j < w; j++) {
                arr[i].push(val);
            }
        }
        return arr;
    }

    resultString() {
        var output = '';
        this.boardInputGrid.forEach((res) => {
            res.forEach((res2) => {
                output += res2;
            });
        });
        return output;
    }

    setResult(indexOne, indexTwo, event) {
        this.boardInputGrid[indexOne][indexTwo] = event.target.value;
        this.setState({ result: this.boardInputGrid });

        this.moveGridInputCursorToNextInputBox(indexOne, indexTwo);
    }

    moveGridInputCursorToNextInputBox(indexOne: number, indexTwo: number) {
        var nextLocationIndex = indexOne + '' + (indexTwo + 1);
        if (nextLocationIndex[1] >= this.state.gridSize) {
            if ((indexOne + 1) < this.state.gridSize) {
                nextLocationIndex = (indexOne + 1) + '' +  0;
            }
        }

        var nextInput = document.getElementById(nextLocationIndex);
        nextInput.focus();
    }

    getResults() {
        var self = this;
        this.props.setIsLoadingFunction(true);
        this.props.setShowResultsFunction(false);
        var array = this.state.wordLengths.slice(0);
        var myHeaders = new Headers({'Accept': 'application/json', 'Content-Type': 'application/json',});
        array.pop();
        return fetch('https://wordbrainspuzzlesolver.azurewebsites.net/api/FindWords', {
            method: 'POST',
            headers: myHeaders,
            body: JSON.stringify({
                WordLength: array,
                Board: this.resultString(),
            })
        }).then(function (response) {
            response.json().then(response => {
                if (!!response.ExceptionMessage) {
                    self.props.setErrorMessageFunction('Somethings went wrong :( \n ' + response.ExceptionMessage);
                } else {
                    self.props.setFoundWordsFunction(response);
                }
                self.props.setIsLoadingFunction(false);
                self.props.setShowResultsFunction(true);
            });
        }).catch(() => {
            self.props.setErrorMessageFunction('something went wrong :(');
            self.props.setIsLoadingFunction(false);
            self.props.setShowResultsFunction(true);
        })
    }
}