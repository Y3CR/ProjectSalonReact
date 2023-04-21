import "bootstrap/dist/css/bootstrap.min.css"; //recurso bootstrap
//import { useEffect, useState } from "react";
import Navbar from "./Components/Navbar";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Home from "./Components/Home";
import About from "./Components/About";
import Skills from "./Components/Skills";
import Services from "./Components/Services";
import Contact from "./Components/Contact";
import Producto from "./Components/Producto";
//import { Children } from "react";

const App = () => {
    return (
        <div>
            <Router>
                <Navbar />
                <Switch>
                    <Route path="/" component={Home} exact>
                        <Home />
                    </Route>
                    <Route path="/producto" component={Producto} exact>
                        <Producto />
                    </Route>
                    <Route path="/about" component={About} exact>
                        <About />
                    </Route>
                    <Route path="/skills" component={Skills} exact>
                        <Skills />
                    </Route>
                    <Route path="/services" component={Services} exact>
                        <Services />
                    </Route>
                    <Route path="/contact" component={Contact} exact>
                        <Contact />
                    </Route>
                </Switch>
            </Router>
        </div>


    );
};

export default App;
