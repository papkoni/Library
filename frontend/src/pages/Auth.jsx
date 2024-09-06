import React, { useContext, useState } from "react";
import { Context } from "../main.jsx";
import { Container, Form } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import Row from "react-bootstrap/Row";
import { NavLink, useLocation, useNavigate } from "react-router-dom";
import { LOGIN_ROUTE, REGISTRATION_ROUTE, LIBRARY_ROUTE } from "../utils/consts";
import { login } from '../utils/api/userApi';
import { registration } from '../utils/api/userApi';


const Auth = observer(() => {
    const { user } = useContext(Context);
    const location = useLocation();
    const isLogin = location.pathname === LOGIN_ROUTE;
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState(''); // Для регистрации
    const [resultMessage, setResultMessage] = useState('');
    const navigate = useNavigate(); 


    const handleLogin = async () => {
        try {
            const result = await login({ email, password });

            if (result != null) {
                user.setUser(result);
               
                user.setIsAuth(true);
                setResultMessage('Login successful!');
                navigate(LIBRARY_ROUTE); 
                console.log('Login successful!');
            } else if (typeof result === 'string') {
                setResultMessage(result);
            }
        } catch (error) {
            console.error('Error during login:', error);
            setResultMessage('Login failed!');
            user.setIsAuth(false);
            throw error;
        }
    };

    const handleRegistration = async () => {
        try {
            const result = await registration({ email, password, name });

            if (result === true) {
                user.setIsAuth(true);
                setResultMessage('Registration successful!');
                console.log("Registration successful!");
                navigate(LIBRARY_ROUTE); 
            } else if (typeof result === 'string') {
                setResultMessage(result);
            }
        } catch (error) {
            console.error('Error during registration:', error);
            setResultMessage('Registration failed!');
            navigate(LIBRARY_ROUTE); 
            user.setIsAuth(false);
            throw error;
        }
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        if (isLogin) {
            handleLogin();
        } else {
            handleRegistration();
        }
    };

    return (
        <Container className="d-flex justify-content-center align-items-center" style={{ height: window.innerHeight - 54 }}>
            <Card style={{ width: 600 }} className="p-5">
                <h2 className="m-auto">{isLogin ? 'Авторизация' : "Регистрация"}</h2>
                <Form className="d-flex flex-column" onSubmit={handleSubmit}>
                    <Form.Control
                        className="mt-3"
                        placeholder="Введите ваш email..."
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <Form.Control
                        className="mt-3"
                        placeholder="Введите ваш пароль..."
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    {!isLogin && (
                        <Form.Control
                            className="mt-3"
                            placeholder="Введите ваше имя..."
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                    )}
                    <Row className="d-flex justify-content-between mt-3 pl-3 pr-3">
                        {isLogin ? (
                            <div>
                                Нет аккаунта? <NavLink to={REGISTRATION_ROUTE}>Зарегистрируйся!</NavLink>
                            </div>
                        ) : (
                            <div>
                                Есть аккаунт? <NavLink to={LOGIN_ROUTE}>Войдите!</NavLink>
                            </div>
                        )}
                        <Button  variant={"outline-success"} type="submit" >
                            {isLogin ? 'Войти' : 'Регистрация'} 
                        </Button>
                    </Row>
                </Form>
                {resultMessage && <div className="mt-3">{resultMessage}</div>}
            </Card>
        </Container>
    );
});

export default Auth;
