import React from 'react';
import { GraduationCap } from 'lucide-react';

export const Footer = ({ onOpenSchools, onOpenCategories }) => {
  return (
    <footer className="footer">
      <div className="footer-container">
        <div className="footer-brand">
          <a href="#" className="brand-logo">
            <div className="brand-icon-wrapper">
              <GraduationCap size={20} />
            </div>
            <span className="brand-name">EducaFind</span>
          </a>
          <p>A maior plataforma de descoberta, comparação e ranqueamento de cursos e instituições de ensino do Brasil.</p>
        </div>

        <div className="footer-col">
          <h4>Navegação</h4>
          <ul className="footer-links">
            <li><a href="#">Início</a></li>
            <li><button onClick={onOpenSchools}>Escolas Parceiras</button></li>
            <li><a href="#coursesSection">Cursos em Destaque</a></li>
            <li><button onClick={onOpenCategories}>Categorias</button></li>
          </ul>
        </div>

        <div className="footer-col">
          <h4>Institucional</h4>
          <ul className="footer-links">
            <li><a href="#">Sobre nós</a></li>
            <li><a href="#">Para Escolas / Empresas</a></li>
            <li><a href="#">Termos de Uso</a></li>
            <li><a href="#">Privacidade</a></li>
          </ul>
        </div>

        <div className="footer-col">
          <h4>Contato & Suporte</h4>
          <ul className="footer-links">
            <li><a href="#">Central de Ajuda</a></li>
            <li><a href="#">contato@educafind.com.br</a></li>
            <li><a href="#">São Paulo - SP, Brasil</a></li>
          </ul>
        </div>
      </div>

      <div className="footer-bottom">
        <p>&copy; {new Date().getFullYear()} EducaFind. Desenvolvido em React moderno.</p>
        <p>Conectando seu potencial ao futuro da educação.</p>
      </div>
    </footer>
  );
};
