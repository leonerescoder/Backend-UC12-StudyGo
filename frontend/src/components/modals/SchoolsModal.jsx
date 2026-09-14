import React from 'react';
import { X, Trophy } from 'lucide-react';
import { SCHOOLS_DATA } from '../../data/mockData';

export const SchoolsModal = ({ isOpen, onClose }) => {
  if (!isOpen) return null;

  const getRankClass = (rank) => {
    if (rank === 1) return 'school-rank-1';
    if (rank === 2) return 'school-rank-2';
    if (rank === 3) return 'school-rank-3';
    return 'school-rank-other';
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-container" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h3 className="modal-title">
            <Trophy size={22} style={{ color: '#f59e0b' }} />
            <span>Ranking das Melhores Escolas</span>
          </h3>
          <button className="modal-close-btn" onClick={onClose} aria-label="Fechar modal">
            <X size={20} />
          </button>
        </div>

        <div className="modal-body">
          <div className="schools-list">
            {SCHOOLS_DATA.map((school) => (
              <div key={school.rank} className="school-item">
                <div className={`school-rank-badge ${getRankClass(school.rank)}`}>
                  #{school.rank}
                </div>
                <div className="school-details">
                  <div className="school-name">{school.name}</div>
                  <div className="school-stats">
                    <span>{school.area}</span>
                    <span>•</span>
                    <span>{school.coursesCount} cursos</span>
                  </div>
                </div>
                <div className="school-rating">⭐ {school.rating}</div>
              </div>
            ))}
          </div>

          <div className="modal-footer-actions" style={{ marginTop: '1.5rem' }}>
            <button className="btn-secondary-action" onClick={onClose}>
              Fechar
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
