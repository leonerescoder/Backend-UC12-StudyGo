import React from 'react';
import { X, Folder } from 'lucide-react';
import { CATEGORIES_DATA } from '../../data/mockData';

export const CategoriesModal = ({ isOpen, onClose, onSelectCategory }) => {
  if (!isOpen) return null;

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-container" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h3 className="modal-title">
            <Folder size={22} style={{ color: 'var(--accent-cyan)' }} />
            <span>Explorar por Áreas do Conhecimento</span>
          </h3>
          <button className="modal-close-btn" onClick={onClose} aria-label="Fechar modal">
            <X size={20} />
          </button>
        </div>

        <div className="modal-body">
          <div className="category-modal-grid">
            {CATEGORIES_DATA.filter(c => c.id !== 'all').map((category) => (
              <button
                key={category.id}
                className="category-card-btn"
                onClick={() => {
                  onSelectCategory(category.name);
                  onClose();
                }}
              >
                <span className="category-icon">{category.icon}</span>
                <span className="category-name">{category.name}</span>
                <span className="category-count">{category.count}</span>
              </button>
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
