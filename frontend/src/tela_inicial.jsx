import React, { useState, useMemo } from 'react';
import { Navbar } from './components/Navbar';
import { Hero } from './components/Hero';
import { Features } from './components/Features';
import { CourseGrid } from './components/CourseGrid';
import { Footer } from './components/Footer';
import { Toast } from './components/Toast';
import { CourseModal } from './components/modals/CourseModal';
import { SchoolsModal } from './components/modals/SchoolsModal';
import { CategoriesModal } from './components/modals/CategoriesModal';
import { COURSES_DATA } from './data/mockData';

import './styles/components.css';
import './styles/modal.css';

export const TelaInicial = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [activeCategory, setActiveCategory] = useState('Todos');
  const [selectedCourse, setSelectedCourse] = useState(null);
  const [isSchoolsOpen, setIsSchoolsOpen] = useState(false);
  const [isCategoriesOpen, setIsCategoriesOpen] = useState(false);
  const [toastMessage, setToastMessage] = useState('');

  const triggerToast = (msg) => {
    setToastMessage(msg);
    setTimeout(() => {
      setToastMessage('');
    }, 3500);
  };

  const filteredCourses = useMemo(() => {
    return COURSES_DATA.filter((course) => {
      const matchesCategory =
        activeCategory === 'Todos' ||
        course.category.toLowerCase().includes(activeCategory.toLowerCase());

      const query = searchTerm.trim().toLowerCase();
      const matchesSearch =
        !query ||
        course.title.toLowerCase().includes(query) ||
        course.category.toLowerCase().includes(query) ||
        course.school.toLowerCase().includes(query) ||
        course.description.toLowerCase().includes(query);

      return matchesCategory && matchesSearch;
    });
  }, [searchTerm, activeCategory]);

  const handleHeroSearchSubmit = (e) => {
    e.preventDefault();
    const element = document.getElementById('coursesSection');
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  };

  const handleScrollToCourses = () => {
    const element = document.getElementById('coursesSection');
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  };

  const handleEnrollCourse = (course) => {
    setSelectedCourse(null);
    triggerToast(`🎉 Inscrição iniciada com sucesso em: "${course.title}"!`);
  };

  const handleResetFilters = () => {
    setSearchTerm('');
    setActiveCategory('Todos');
  };

  return (
    <div className="tela-inicial-page">
      {/* 2.1 Barra de Navegação Superior (Header / Navbar) */}
      <Navbar
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        onOpenSchools={() => setIsSchoolsOpen(true)}
        onOpenCategories={() => setIsCategoriesOpen(true)}
      />

      <main>
        {/* 2.2 Seção Hero (Destaque Principal) */}
        <Hero
          searchTerm={searchTerm}
          onSearchChange={setSearchTerm}
          onSearchSubmit={handleHeroSearchSubmit}
        />

        {/* 2.3 Barra de Diferenciais / Estatísticas (Feature Cards) */}
        <Features
          onOpenSchools={() => setIsSchoolsOpen(true)}
          onScrollToCourses={handleScrollToCourses}
          onCertificatesClick={() => triggerToast('📜 Todos os nossos cursos emitem certificados verificados!')}
        />

        {/* 2.4 Seção de Cursos em Destaque */}
        <CourseGrid
          courses={filteredCourses}
          activeCategory={activeCategory}
          onSelectCategory={(cat) => {
            setActiveCategory(cat);
            handleScrollToCourses();
          }}
          onSelectCourse={(course) => setSelectedCourse(course)}
          onResetFilters={handleResetFilters}
        />
      </main>

      {/* Rodapé da Página */}
      <Footer
        onOpenSchools={() => setIsSchoolsOpen(true)}
        onOpenCategories={() => setIsCategoriesOpen(true)}
      />

      {/* Modais Interativos */}
      <CourseModal
        course={selectedCourse}
        onClose={() => setSelectedCourse(null)}
        onEnroll={handleEnrollCourse}
      />

      <SchoolsModal
        isOpen={isSchoolsOpen}
        onClose={() => setIsSchoolsOpen(false)}
      />

      <CategoriesModal
        isOpen={isCategoriesOpen}
        onClose={() => setIsCategoriesOpen(false)}
        onSelectCategory={(cat) => {
          setActiveCategory(cat);
          handleScrollToCourses();
        }}
      />

      {/* Notificações Toast */}
      <Toast message={toastMessage} onClose={() => setToastMessage('')} />
    </div>
  );
};

export default TelaInicial;
