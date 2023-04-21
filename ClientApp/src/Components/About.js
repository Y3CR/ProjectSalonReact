import React, { useState, useEffect } from 'react';
import { CardGroup, Card } from 'react-bootstrap';

const imageReact = require.context('../images', true);

const About = (props) => {

    const [productos, setProductos] = useState([])



    useEffect(() => {
        fetch(`/api/product/Cards`)
            .then(response => response.json())
            .then(data => setProductos(data));
    }, []);



    return (

        <section className="hero">

            <CardGroup>
                {productos.map(item => (
                    <Card key={item.idProduct} style={{ width: '18rem' }}>
                        <Card.Img variant="top" src={imageReact(`./${item.imagePath.split('\\').pop().split('/').pop()}`)}
                            alt={item.nameProduct} />
                            
                          
                        <Card.Body>
                            <Card.Title>{item.nameProduct}</Card.Title>
                            <Card.Text>
                                {item.description}
                            </Card.Text>
                            <Card.Text>
                                {item.stocks}
                            </Card.Text>
                        </Card.Body>
                    </Card>
                ))}
            </CardGroup>
        </section>
    );
};
export default About;
