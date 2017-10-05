import * as React from 'react';

interface IHomeProps { };

interface IHomeState {
  gridSize,
  result,
  errorMessage,
  foundWords,
  wordLengths
};

export class Home extends React.Component<IHomeProps, IHomeState> {
  constructor() {
    super()
    this.state = {
      gridSize: 3,
      result: [],
      errorMessage: '',
      foundWords: '',
      wordLengths: ''
    }
    this.setState({ result: this.makeArray(this.state.gridSize, this.state.gridSize, '') });
  }
  setResult(indexOne, indexTwo, event) {
    var updatedResult = this.state.result;
    updatedResult[indexOne][indexTwo] = event.target.value;
    this.setState({ result: updatedResult });
  }
  setGridSize(event) {
    if (event.target.value > 5) {
      this.setState({ errorMessage: 'You cannot set a grid size greater than 5' })
      return;
    } else {
      this.setState({ errorMessage: '' })
    }

    this.setState({ gridSize: event.target.value });
    this.setState({ result: this.makeArray(event.target.value, event.target.value, '') });
  }
  resultString() {
    var output = '';
    this.state.result.forEach((res) => {
      res.forEach((res2) => {
        output += res2;
      });
    });
    return output;
  }
  getResults() {
    var self = this;
    
    fetch('http://localhost/wordbrainsolver/api/FindWords', {  
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        WordLength: this.state.wordLengths,
        Board: this.getResults(),
      })
    })


    fetch('https://jsonplaceholder.typicode.com/posts/1').then(function(response){
      if (response.ok) {
        response.json().then(response => {
          self.setState({ foundWords: response.body });
        });
      }
    })
  }
  setWordLengths(event) {
    this.setWordLengths({ wordLengths: event.target.value });
  }
  render() {
    return (
      <div>
        Please enter the grid size
        <input value={this.state.gridSize} onChange={(event) => this.setGridSize(event)} />
        <input value={this.state.wordLengths} onChange={(event) => this.setWordLengths(event)} />
        <ul>
          {
            this.state.result.map((res, index) => (
              <li>
                {res.map((resTwo, indexTwo) => (
                  <input onChange={(event) => this.setResult(index, indexTwo, event)} />
                ))}
              </li>
            ))
          }
        </ul>
        <p>{this.state.errorMessage}</p>
        <p>{this.resultString()}</p>
        <button onClick={(event) => this.getResults()}>Get results</button>
        <p>{this.state.foundWords}</p>
      </div>
    );
  }
  makeArray(w, h, val) {
    var arr = [];
    for (var i = 0; i < h; i++) {
      arr[i] = [];
      for (var j = 0; j < w; j++) {
        arr[i][j] = val;
      }
    }
    return arr;
  }
}
