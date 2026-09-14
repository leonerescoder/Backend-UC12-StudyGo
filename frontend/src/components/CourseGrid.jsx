import React from 'react';
import { ArrowRight, SearchX } from 'lucide-react';
import { CourseCard } from './CourseCard';
import { CATEGORIES_DATA } from '../data/mockData';

export const CourseGrid = ({ 
  courses, 
  activeCategory, 
  onSelectCategory, 
  onSelectCourse, 
  onResetFilters 
}) => {
  return (
    <section className="courses-section" id="coursesSection">
      <div className="container">
        
        {/* Header da Seção */}
        <div className="section-header">
          <div className="section-title-group">
            <div className="section-indicator"></div>
            <h2 className="section-title">Cursos em destaque</h2>
          </div>
          <button 
            type="button"
            onClick={() => onSelectCategory('Todos')} 
            className="view-all-link"
          >
            <span>Ver todos</span>
            <ArrowRight size={18} />
          </button>
        </div>

        {/* Filter Tabs */}
        <div className="filter-tabs">
          {CATEGORIES_DATA.map((cat) => (
            <button
              key={cat.id}
              className={`filter-tab ${activeCategory === cat.name ? 'active' : ''}`}
              onClick={() => onSelectCategory(cat.name)}
            >
              <span>{cat.icon}</span>
              <span>{cat.name}</span>
            </button>
          ))}
        </div>

        {/* Grid de Cursos */}
        <div className="courses-grid">
          {courses.length > 0 ? (
            courses.map((course) => (
              <CourseCard 
                key={course.id} 
                course={course} 
                onSelectCourse={onSelectCourse} 
              />
            ))
          ) : (
            <div className="no-results">
              <div className="no-results-icon">
                <SearchX size={48} style={{ color: 'var(--accent-cyan)' }} />
              </div>
              <h3>Nenhum curso encontrado</h3>
              <p style={{ color: 'var(--text-muted)', marginTop: '0.5rem' }}>
                Tente pesquisar com outros termos ou redefinir seus filtros.
              </p>
              <button 
                className="btn-primary-action" 
                style={{ marginTop: '1.25rem' }}
                onClick={onResetFilters}
              >
                Limpar Busca e Filtros
              </button>
            </div>
          )}
        </div>

      </div>
    </section>
  );
};
