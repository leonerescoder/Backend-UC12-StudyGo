import React from 'react';
import { Sparkles } from 'lucide-react';

export const Toast = ({ message, onClose }) => {
  if (!message) return null;

  return (
    <div className="toast-container">
      <div className="toast">
        <Sparkles size={18} style={{ color: 'var(--accent-cyan)' }} />
        <div>{message}</div>
      </div>
    </div>
  );
};
