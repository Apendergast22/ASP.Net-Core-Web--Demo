import './Modal.css'
import React from 'react'

const Modal = ({ active, setActive, children})=> {
    return(
        <div className={active ? 'modal-background active' : 'modal-background'} onClick={() => setActive(false)}>
            <div className={active ? 'modal-content active' : 'modal-content'} onClick={e => e.stopPropagation()}>
                {children}
            </div>
        </div>
    );
};

export default Modal;
