import React from 'react';
import { Search } from 'lucide-react';

export const Hero = ({ searchTerm, onSearchChange, onSearchSubmit }) => {
  return (
    <section className="hero-section" id="heroSection">
      <div className="container hero-content">
        {/* Badge Superior */}
        <div className="hero-badge">
          <span>🎓</span>
          <span>Seu futuro começa aqui</span>
        </div>

        {/* Headline */}
        <h1 className="hero-headline">
          Encontre o <span className="highlight">curso</span> ideal para o <span className="highlight">seu futuro.</span>
        </h1>

        {/* Subtitle */}
        <p className="hero-subtitle">
          Mais de 100 escolas e milhares de cursos em um só lugar.
        </p>

        {/* Hero Search Bar */}
        <div className="hero-search-wrapper">
          <form className="hero-search-form" onSubmit={onSearchSubmit}>
            <Search className="hero-search-icon" size={20} />
            <input 
              type="text" 
              className="hero-search-input" 
              placeholder="Pesquisar cursos, escolas ou áreas..." 
              value={searchTerm}
              onChange={(e) => onSearchChange(e.target.value)}
              autoComplete="off"
            />
            <button type="submit" className="hero-search-btn">
              <Search size={18} strokeWidth={2.5} />
              <span>Buscar</span>
            </button>
          </form>
        </div>
      </div>
    </section>
  );
};
