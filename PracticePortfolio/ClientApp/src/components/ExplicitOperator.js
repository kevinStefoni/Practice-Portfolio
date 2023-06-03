import React, { Component } from 'react';
import { CodeSnippet } from './CodeSnippet';

export class ExplicitOperator extends Component {
    constructor(props) {
        super(props);
        this.state = {
            demoCode: '',
            codeSnippet: '',
            result: '',
            kilograms: 5.231 
        };
    }

    componentDidMount() {
        this.fetchCodeSnippet();
        this.fetchResult();
    }

    fetchCodeSnippet = async () => {
        try {
            let response = await fetch('/ExplicitOperatorDemo.txt');
            let codeContent = await response.text();
            this.setState({ demoCode: codeContent });
        }
        catch {
            this.setState({ demoCode: 'Unable to load code.' });
        }

        try {
            let response = await fetch('/ImperialPound.txt');
            let codeContent = await response.text();
            this.setState({ codeSnippet: codeContent });
        } catch {
            this.setState({ codeSnippet: 'Unable to load code.' });
        }
    };

    fetchResult = async () => {
        try {
            let response = await fetch(`/Csharp/explicit-operator?kilograms=${this.state.kilograms}`);
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
        const { kilograms } = this.state;

        return (
            <div>
                <CodeSnippet codeSnippet={demoCode} />
                {result && (
                    <div>
                        <p>{kilograms} kilograms is { result.pounds } pounds</p>
                    </div>
                )}
                <CodeSnippet codeSnippet={codeSnippet} />
            </div>
        );



    }
}
