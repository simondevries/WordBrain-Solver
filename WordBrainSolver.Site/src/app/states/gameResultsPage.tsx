import * as React from 'react';

interface IGameResultsPageProps {
    errorMessage,
    foundWords,
    resetFunction
};

export class GameResultsPage extends React.Component<IGameResultsPageProps> {
    render() {
        return (
            <div>
                <h3 className='error-message-content'>{this.props.errorMessage}</h3>
                <h3 className='result-message-content'>{this.props.foundWords}</h3>
                <button onClick={(event) => this.props.resetFunction()} className='my-button'>Back</button>
            </div>
        );
    }
}