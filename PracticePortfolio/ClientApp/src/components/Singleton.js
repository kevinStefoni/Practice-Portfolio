import React, { Component } from 'react';
import { CodeSnippet } from './CodeSnippet';

export class Singleton extends Component {
    constructor(props) {
        super(props);
        this.state = {
            codeSnippet: '',
        };
    }

    componentDidMount() {
        this.fetchCodeSnippet();
    }

    fetchCodeSnippet = async () => {
        try {
            const response = await fetch('/Singleton.cs');
            const codeContent = await response.text();
            this.setState({ codeSnippet: codeContent });
        } catch (error) {
            this.setState({ codeSnippet: 'Unable to load code.' });
        }
    };

    render() {
        const { codeSnippet } = this.state;

        
        return <CodeSnippet codeSnippet={codeSnippet} />;

    }
}
