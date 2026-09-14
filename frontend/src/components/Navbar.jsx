import React from 'react';
import { Menu, Search, GraduationCap } from 'lucide-react';

export const Navbar = ({ 
  searchTerm, 
  onSearchChange, 
  onOpenSchools, 
  onOpenCategories,
  onMenuToggle
}) => {
  return (
    <header className="navbar-mestre-wrapper">
      <div className="navbar-mestre-container">
        
        {/* Lado Esquerdo: Hamburger + Logo EducaFind */}
        <div className="nav-left-group">
          <button 
            type="button" 
            className="nav-hamburger-btn" 
            onClick={onMenuToggle || onOpenCategories}
            aria-label="Abrir menu"
          >
            <Menu size={26} strokeWidth={2.2} />
          </button>

          <a href="#" className="nav-mestre-brand">
            <div className="nav-mestre-icon-box">
              {/* Capelo estilizado como em navbar_mestre.png */}
              <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M12 3L1 9L12 15L21 10.09V17H23V9L12 3Z" fill="#1d4ed8"/>
                <path d="M5 13.18V17.18C5 19.84 8.13 22 12 22C15.87 22 19 19.84 19 17.18V13.18L12 17L5 13.18Z" fill="#2563eb" fillOpacity="0.85"/>
              </svg>
            </div>
            <span className="nav-mestre-title">EducaFind</span>
          </a>
        </div>

        {/* Centro: Barra de Busca em Pílula */}
        <div className="nav-mestre-search">
          <Search className="nav-mestre-search-icon" size={20} strokeWidth={2.2} />
          <input 
            type="text" 
            className="nav-mestre-search-input" 
            placeholder="Pesquisar cursos, escolas ou áreas..." 
            value={searchTerm}
            onChange={(e) => onSearchChange(e.target.value)}
            aria-label="Pesquisar cursos, escolas ou áreas"
          />
        </div>

        {/* Lado Direito: Links de Navegação */}
        <nav className="nav-mestre-links">
          <a href="#" className="nav-mestre-link active">Início</a>
          <button type="button" onClick={onOpenSchools} className="nav-mestre-link">Escolas</button>
          <a href="#coursesSection" className="nav-mestre-link">Cursos</a>
          <button type="button" onClick={onOpenCategories} className="nav-mestre-link">Categorias</button>
        </nav>

      </div>
    </header>
  );
};
