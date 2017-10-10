import * as React from 'react';
import './home.less';
import '../../images/background.jpg';

interface IHomeProps { };

interface IHomeState {
  gridSize,
  result,
  errorMessage,
  foundWords,
  wordLengths,
  showResult,
  isLoading
};

export class Home extends React.Component<IHomeProps, IHomeState> {
  constructor() {
    super()
    var size = '3';
    var array = this.makeArray(size, size, '');

    this.state = {
      gridSize: size,
      result: array,
      errorMessage: '',
      foundWords: '',
      wordLengths: [''],
      showResult: false,
      isLoading: false
    }
    this.setState({ result: array });
    // this.setGridSize(3);
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

  setWordLengths(event, index) {
    var wordLengths = this.state.wordLengths;
    if (index === wordLengths.length - 1) {
      wordLengths.push('');
    }
    wordLengths[index] = event.target.value;
    this.setState({ wordLengths: wordLengths });
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
    
    this.setState({isLoading: true});
    this.setState({showResult: false});
    var array = this.state.wordLengths.slice(0);
    array.pop();
    return fetch('https://wordbrainspuzzlesolver.azurewebsites.net/api/FindWords', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        WordLength: array,
        Board: this.resultString(),
      })
    }).then(function (response) {
      response.json().then(response => {
        if(!!response.ExceptionMessage){
          self.setState({ errorMessage: 'Somethings went wrong :( \n ' + response.ExceptionMessage});
        } else{
          self.setState({ foundWords: response });
        }
        self.setState({isLoading: false});
        self.setState({showResult: true});
      });
    }).catch(()=>{
      self.setState({errorMessage: 'something went wrong :('})
      self.setState({isLoading: false});
      self.setState({showResult: true});
    })
  }

  render() {
    return (
      <div className='container'>
        <h1>Word brain puzzle solver</h1>
        <div className={!this.state.showResult && !this.state.isLoading ? 'page' : 'hidden'}>
          <h2>Grid size:</h2>
          <div>
            <input className='number-input-field' value={this.state.gridSize} onChange={(event) => this.setGridSize(event)} maxLength={1} />
          </div>
          <h2>Word Lengths:</h2>
          <ul>
            {
              this.state.wordLengths.map((res, index) => (
                <input className='number-input-field word-length-input' value={this.state.wordLengths[index]} onChange={(event) => this.setWordLengths(event, index)}  maxLength={1}/>
              ))
            }
          </ul>
          <h2>Grid:</h2>
          <ul className='input-grid'>
            {
              this.state.result.map((res, index) => (
                <li>
                  {res.map((resTwo, indexTwo) => (
                    <input className='grid-input-field number-input-field' onChange={(event) => this.setResult(index, indexTwo, event)}  maxLength={1}/>
                  ))}
                </li>
              ))
            }
          </ul>
          <div className='bottom-div'>
            <button className='my-button' onClick={(event) => this.getResults()}>Get results</button>
          </div>
        </div>
        <div className={this.state.showResult && !this.state.isLoading ? 'page' : 'hidden'}>
          <h3 className='error-message-content'>{this.state.errorMessage}</h3>
          <h3 className='result-message-content'>{this.state.foundWords}</h3>
          <button onClick={(event) => this.reset()} className='my-button'>Back</button>
        </div>
        <div className={this.state.isLoading ? 'loading page' : 'hidden loading'}>~ L O A D I N G ~</div>
      </div>
    );
  }

  reset() {
    this.setState({ showResult: false });
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
