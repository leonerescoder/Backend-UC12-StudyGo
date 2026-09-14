import React from 'react';

export const CourseCard = ({ course, onSelectCourse }) => {
  return (
    <div className="course-card" onClick={() => onSelectCourse(course)}>
      <div className="course-banner">
        {course.badge && (
          <span className={`card-badge-top ${course.badgeType}`}>
            {course.badge}
          </span>
        )}
        {course.renderBanner()}
      </div>
      <div className="course-body">
        <div className="course-category">{course.category}</div>
        <h3 className="course-title">{course.title}</h3>
        <div className="course-details">
          <span className="course-workload">⏱️ {course.workload}</span>
          <span className="course-school" title={course.school}>🏫 {course.school}</span>
        </div>
      </div>
    </div>
  );
};
