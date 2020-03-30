import React, { Component } from 'react';

interface FetchDataState {
    loading: boolean;
    Animals: Animals[]
}

interface Animals {
    id: number;
    name: string;
    description: string;
    age: number;
    animalType: string;    
}

export class Home extends Component<{}, FetchDataState> {

    constructor(props: any) {
        super(props);
        this.state = { Animals: [], loading: true };

        fetch('api/animalinformation')
            .then(response => response.json())
            .then(data => {
                this.setState({ Animals: data, loading: false });
            });
    }

    static renderAnimalsTable(Animals: Animals[]) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Age</th>
                        <th>Animal Type</th>
                        <th>Image</th>
                    </tr>
                </thead>
                <tbody>
                    {Animals.map(animal =>
                        <tr key={animal.id}>
                            <td>{animal.id}</td>
                            <td>{animal.name}</td>
                            <td>{animal.description}</td>
                            <td>{animal.age}</td>
                            <td>{animal.animalType}</td>
                            <td><img alt={animal.name} src={`/api/image/${animal.id}`} height="100px" width="100px"/></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderAnimalsTable(this.state.Animals);

        return (
            <div>
                <h1>Animal Information</h1>                
                {contents}
            </div>
        );
    }
}
