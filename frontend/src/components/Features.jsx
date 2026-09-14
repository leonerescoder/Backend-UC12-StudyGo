import React from 'react';
import { School, BookOpen, Trophy, Award } from 'lucide-react';

export const Features = ({ onOpenSchools, onScrollToCourses, onCertificatesClick }) => {
  return (
    <section className="features-section" id="featuresSection">
      <div className="container">
        <div className="features-grid">
          
          {/* Card 1: Escolas */}
          <div className="feature-card" onClick={onOpenSchools}>
            <div className="feature-icon-wrapper">
              <School size={24} />
            </div>
            <div className="feature-info">
              <span className="feature-tag">Instituições</span>
              <span className="feature-title">Mais de 100 escolas</span>
            </div>
          </div>

          {/* Card 2: Cursos */}
          <div className="feature-card" onClick={onScrollToCourses}>
            <div className="feature-icon-wrapper">
              <BookOpen size={24} />
            </div>
            <div className="feature-info">
              <span className="feature-tag">Variedade</span>
              <span className="feature-title">Milhares de cursos</span>
            </div>
          </div>

          {/* Card 3: Ranking */}
          <div className="feature-card" onClick={onOpenSchools}>
            <div className="feature-icon-wrapper">
              <Trophy size={24} />
            </div>
            <div className="feature-info">
              <span className="feature-tag">Qualidade</span>
              <span className="feature-title">Ranking das melhores escolas</span>
            </div>
          </div>

          {/* Card 4: Certificação */}
          <div className="feature-card" onClick={onCertificatesClick}>
            <div className="feature-icon-wrapper">
              <Award size={24} />
            </div>
            <div className="feature-info">
              <span className="feature-tag">Garantia</span>
              <span className="feature-title">Certificados reconhecidos</span>
            </div>
          </div>

        </div>
      </div>
    </section>
  );
};
