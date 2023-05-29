import React, { Component } from 'react';
import { CodeSnippet } from './CodeSnippet';

export class Singleton extends Component {
    constructor(props) {
        super(props);
        this.state = {
            demoCode: '',
            codeSnippet: '',
            result: '',
            value: 5,
            newValue: 10,
        };
    }

    componentDidMount() {
        this.fetchCodeSnippet();
        this.fetchResult();
    }

    fetchCodeSnippet = async () => {
        try {
            let response = await fetch('/SingletonDemo.txt');
            let codeContent = await response.text();
            this.setState({ demoCode: codeContent });
        }
        catch {
            this.setState({ demoCode: 'Unable to load code.' });
        }

        try {
            let response = await fetch('/Singleton.txt');
            let codeContent = await response.text();
            this.setState({ codeSnippet: codeContent });
        } catch {
            this.setState({ codeSnippet: 'Unable to load code.' });
        }
    };

    fetchResult = async () => {
        try {
            let response = await fetch(`/DesignPatterns/singleton?value=${this.state.value}&newValue=${this.state.newValue}`);
            if (response.ok) {
                const result = await response.json();
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
        const { value } = this.state;
        const { newValue } = this.state;
        
        return (
            <div>
                <CodeSnippet codeSnippet={demoCode} />
                {result && (
                    <div>
                        <p>value: {value}</p>
                        <p>newValue: {newValue}</p>
                        <hr />
                        <p>First Singleton Value: {result.firstInstance.value}</p>
                        <p>Second Singleton Value: {result.secondInstance.value}</p>
                    </div>
                )}
                <CodeSnippet codeSnippet={codeSnippet} />
            </div>
        );

        

    }
}
