import React, { Component } from 'react';
import './Home.css'

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            greeting: '',
        };
    }

    componentDidMount() {
        const currentHour = new Date().getHours();

        if (currentHour >= 3 && currentHour < 12) {
            this.setState({ greeting: 'Good Morning' });
        } else if (currentHour >= 12 && currentHour < 18) {
            this.setState({ greeting: 'Good Afternoon' });
        } else if (currentHour >= 18 && currentHour < 21) {
            this.setState({ greeting: 'Good Evening' });
        } else {
            this.setState({ greeting: 'Good Night' });
        }
    }

    render() {
        return (
            <div>
                <h1 className="timed-greeting">{this.state.greeting}</h1>
            </div>
        );
    }
}
