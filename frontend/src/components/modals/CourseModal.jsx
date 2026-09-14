import React from 'react';
import { X, BookOpen, Clock, School, Star, Users } from 'lucide-react';

export const CourseModal = ({ course, onClose, onEnroll }) => {
  if (!course) return null;

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-container" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h3 className="modal-title">
            <BookOpen size={22} style={{ color: 'var(--accent-cyan)' }} />
            <span>{course.title}</span>
          </h3>
          <button className="modal-close-btn" onClick={onClose} aria-label="Fechar modal">
            <X size={20} />
          </button>
        </div>

        <div className="modal-body">
          <div className="course-detail-hero">
            {course.renderBanner()}
          </div>

          <div className="course-meta-tags">
            <span className="meta-pill">📂 {course.category}</span>
            <span className="meta-pill">⏱️ {course.workload}</span>
            <span className="meta-pill">🏫 {course.school}</span>
            <span className="meta-pill">⭐ {course.rating} ({course.studentsCount} alunos)</span>
          </div>

          <p className="course-full-description">{course.description}</p>

          <div className="course-modules-list">
            <h5>📚 O que você vai aprender:</h5>
            <ul>
              {course.modules.map((module, idx) => (
                <li key={idx}>{module}</li>
              ))}
            </ul>
          </div>

          <div className="modal-footer-actions">
            <button className="btn-secondary-action" onClick={onClose}>
              Fechar
            </button>
            <button className="btn-primary-action" onClick={() => onEnroll(course)}>
              Inscrever-se Agora
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
