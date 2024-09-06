
import React, {useContext} from "react";
import { Context } from "../main";
import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import {NavLink} from "react-router-dom";
import { LIBRARY_ROUTE, LOGIN_ROUTE, ADMIN_ROUTE } from "../utils/consts";
import {Button} from "react-bootstrap";
import {observer} from "mobx-react-lite";
import Container from "react-bootstrap/Container";
import {useNavigate} from "react-router-dom"


const NavBar = observer(() => {
    const navigateTo = useNavigate()

    const {user} = useContext(Context)
    return (
        
        <Navbar bg="dark" data-bs-theme="dark">
            <Container>
            <NavLink style={{color:'white'}} to={LIBRARY_ROUTE}>Library</NavLink>
                {user.isAuth ?
                    <Nav className="ms-auto" style={{color: 'white'}}>
                        <Button
                            variant={"outline-light"}
                            onClick={() => navigateTo(ADMIN_ROUTE)}
                        >
                            Админ панель
                        </Button>
                        <Button
                            variant={"outline-light"}
                            onClick={() => navigateTo(LOGIN_ROUTE)}
                            className="ms-2"
                        >
                            Выйти
                        </Button>
                    </Nav>
                    :
                    <Nav className="ms-auto" style={{color: 'white'}}>
                        <Button variant={"outline-light"} onClick={() => user.setIsAuth(true)}>Авторизация</Button>
                    </Nav>
                }
        </Container>
      </Navbar>
      
    
    )
});

export default NavBar;