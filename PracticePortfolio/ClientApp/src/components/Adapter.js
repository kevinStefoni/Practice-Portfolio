import React, { Component } from 'react';
import { CodeSnippet } from './CodeSnippet';

export class Adapter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            demoCode: '',
            codeSnippet: '',
            result: '',
            amount: 49.99,
            cardNumber: '1234567812345678',
            cvv: '123',
        };
    }

    componentDidMount() {
        this.fetchCodeSnippet();
        this.fetchResult();
    }

    fetchCodeSnippet = async () => {
        try {
            let response = await fetch('/AdapterDemo.txt');
            let codeContent = await response.text();
            this.setState({ demoCode: codeContent });
        }
        catch {
            this.setState({ demoCode: 'Unable to load code.' });
        }

        try {
            let response = await fetch('/Adapter.txt');
            let codeContent = await response.text();
            this.setState({ codeSnippet: codeContent });
        } catch {
            this.setState({ codeSnippet: 'Unable to load code.' });
        }
    };

    fetchResult = async () => {
        try {
            let response = await fetch(`/DesignPatterns/adapter?amount=${this.state.amount}&cardNumber=${this.state.cardNumber}&cvv=${this.state.cvv}`);
            if (response.ok) {
                const result = await response.text();
                this.setState({ result: result });
            } else {
                this.setState({ result: null });
            }
        } catch {
            this.setState({ result: null });
        }
    };

    render() {
        const { codeSnippet } = this.state;
        const { demoCode } = this.state;
        const { result } = this.state;
        const { amount } = this.state;
        const { cardNumber } = this.state;
        const { cvv } = this.state;
        
        return (
            <div>
                <CodeSnippet codeSnippet={demoCode} />
                {result && (
                    <div>
                        <p>Amount: {amount}</p>
                        <p>Card Number: {cardNumber}</p>
                        <p>CVV: {cvv}</p>
                        <hr />
                        <p>Payment Statement: {result}</p>
                    </div>
                )}
                <CodeSnippet codeSnippet={codeSnippet} />
            </div>
        );

        

    }
}
