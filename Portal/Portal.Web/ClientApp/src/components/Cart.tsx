import React, { Component } from 'react';

interface CartProps {
    cartId: string;
}

interface CartState {
    animals?: Animals[]
    cart?: UserCart
    isLoading: boolean
}

interface Animals {
    id: number;
    name: string;
}

interface UserCart {
    id: string;
    cartContents: CartContent[]
}

interface CartContent {
    id: number
    quantity: number
}


export class Cart extends Component<CartProps, CartState> {

    constructor(props: any) {
        super(props);
        this.addAnimal = this.addAnimal.bind(this);
        this.removeAnimal = this.removeAnimal.bind(this);

        this.state = {
            isLoading: true
        }

        fetch('api/animalinformation')
            .then(response => response.json())
            .then(data => {
                this.setState((state) => {
                    return {
                        ...state,
                        animals: data
                    }
                });
            });

        fetch(`api/cart/${this.props.cartId}`)
            .then(response => response.json())
            .then(data => {
                this.setState((state) => {
                    return {
                        ...state,
                        cart: data,
                        isLoading: false
                    }
                });
            });

    }

    componentDidUpdate(prevProps: CartProps) {

        if (prevProps.cartId !== this.props.cartId) {
            this.setState({
                ...this.state,
                cart: undefined,
                isLoading: true
            });
            fetch(`api/cart/${this.props.cartId}`)
                .then(response => response.json())
                .then(data => {
                    this.setState((state) => {
                        return {
                            ...state,
                            cart: data,
                            isLoading: false
                        }
                    });
                });
        }
    }

    removeAnimal(animalId: number) {
        this.setState({
            ...this.state,
            isLoading: true
        })
        fetch(`api/cart/${this.props.cartId}/${animalId}`, {
            method: 'DELETE'
        })
            .then(response => response.json())
            .then(data => {
                this.setState((state) => {
                    return {
                        ...state,
                        cart: data,
                        isLoading: false
                    }
                });
            });
    }

    addAnimal(animalId: number) {
        this.setState({
            ...this.state,
            isLoading: true
        });
        // HACK : Visual Studio Online (Beta) support with Chromium 81 (2020-05)
        fetch(`api/cart/post/${this.props.cartId}/${animalId}`)
            .then(response => response.json())
            .then(data => {
                this.setState((state) => {
                    return {
                        ...state,
                        cart: data,
                        isLoading: false
                    }
                });
            });
    }

    renderAnimalsTable(animals: Animals[], cartContents: CartContent[]) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Quantity</th>
                        <th>Increment</th>
                        <th>Decrement</th>
                    </tr>
                </thead>
                <tbody>
                    {animals.map(animal => {
                        let matchingAnimal = cartContents ? cartContents.find(cartAnimal => animal.id === cartAnimal.id) : null;
                        let increment = () => this.addAnimal(animal.id);
                        let decrement = () => this.removeAnimal(animal.id);
                        return <tr key={animal.id}>
                            <td>{animal.id}</td>
                            <td>{animal.name}</td>
                            <td>{matchingAnimal ? matchingAnimal.quantity : 0}</td>
                            <td><button disabled={this.state.isLoading} onClick={increment}>+</button></td>
                            <td><button disabled={this.state.isLoading} onClick={decrement}>-</button></td>
                        </tr>
                    })
                    }
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state && this.state.animals && this.state.cart ? this.renderAnimalsTable(this.state.animals, this.state.cart.cartContents) : "Loading...";
        return (
            <div>
                <h1>Cart</h1>
                {contents}
            </div>
        );
    }
}
